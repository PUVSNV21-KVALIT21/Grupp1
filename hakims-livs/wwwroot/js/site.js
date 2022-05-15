﻿import {createModal, updateModal} from "./components/modal.js";
import { Api } from "./components/api.js";
import LocalStorage from "./components/localStorage.js";
import {CheckoutList} from "./components/checkoutList.js";
import {ProductControls} from "./components/productControls.js";

const cartCounter = document.querySelector('.shoppingCart-counter')


const main = document.getElementById("site")
const modalContainer = document.getElementById("modal-container")
const checkoutButton = document.getElementById("checkoutButton")
const checkoutContainer = document.querySelector('.checkoutContainer');
const makeOrderButton = document.getElementById("MakeOrderButton");
const searchForm = document.getElementById("searchForm");
const searchInput = document.getElementById("searchInput");
const categoriesContainer = document.querySelector(".categories-container")

searchForm.addEventListener('submit', (e) => {
    e.preventDefault();
    let url = "";
    let newUrl = "";
    if (window.location.href.includes("?id=")){
        url = window.location.href.split('&')[0];
        newUrl = url + "&searchString=" + searchInput.value;
    } else {
        url = window.location.href.split('?')[0];
        newUrl = url + "?searchString=" + searchInput.value;
    }
    window.location.replace(newUrl);
})



async function updateCategories() {
    const response = await fetch('/api/Categories');
    const categories = await response.json();
    categories.forEach((category) => {
        const categoryElement = document.createElement('a');
        categoryElement.classList.add("nav-link")
        categoryElement.classList.add("text-dark")
        categoryElement.href = "/Category?id=" + category.id
        categoryElement.textContent = category.name;
        categoriesContainer.appendChild(categoryElement);

    })
}

const updateCounter = (itemsInCart) => {

    if (!itemsInCart) return

    let value = 0;

    itemsInCart.forEach((item) => {
        const price = item.salesPrice;
        if (price) value = value + price
    })
    cartCounter.textContent = value > 0 ? value + " kr" : "";
}



const handleModalClick = (e) => {

    e.preventDefault()
    //do nothing if the click happened within the card
    if (e.target.className !== ('modal-background')){
        return
    }
    
    modalContainer.removeChild(e.target)
    main.className = "";
}
const handleAddClick = async (e) => {
    console.log("click ID: ",e.target.id)
    let itemsInCart = LocalStorage.Get("shoppingCart");
    if (!itemsInCart) itemsInCart = []
    if (e.target.id)
    {
        const product = await Api.getProduct(e.target.id)
        itemsInCart.push(product)
        LocalStorage.Set("shoppingCart", itemsInCart)
        const modalControllers = document.querySelectorAll('.modal-product-controls')
        modalControllers.forEach((controller) => {
            ProductControls(controller, controller.id, handleAddClick, handleRemoveClick)
        })

        updateCounter(itemsInCart)
    } else {
        alert('something went wrong')
    }
}

const handleRemoveClick = async (e) => {
    let itemsInCart = LocalStorage.Get("shoppingCart");
    if (!itemsInCart) return
    const product = await Api.getProduct(e.target.id)
    let removed = false;
    const updatedCart = [];
    itemsInCart.forEach((p) => {
        if (!removed && p.id === product.id)
            removed = true;
        else updatedCart.push(p)

    })
    LocalStorage.Set("shoppingCart", updatedCart)
    const modalControllers = document.querySelectorAll('.modal-product-controls')
    modalControllers.forEach((controller) => {
        ProductControls(controller, controller.id, handleAddClick, handleRemoveClick)
    })
    updateCounter(updatedCart)
}
    


    
const productCards = document.querySelectorAll(".card-product");


function renderProductControls() {
    productCards.forEach(card => {
        let productControls = document.createElement("div")
        productControls.className = "modal-product-controls";
        productControls.id = card.id
        productControls = ProductControls(productControls, card.id, handleAddClick, handleRemoveClick);
        card.appendChild(productControls)
        
    })
}

function addCardEventListeners(){
    productCards.forEach(card => {
    card.addEventListener('click', (event) => {
        handleProductClick(event, card.id)
    })})
}






const handleProductClick = (e, id) => {
    console.log(e.target.className)
    if (e.target instanceof HTMLButtonElement || e.target.classList.contains("control"))
        return;
    
    main.className = "blurred";
    const m = createModal(handleModalClick);
    m.id = id
    modalContainer.appendChild(m)
    Api.getProduct(id).then(product => {
        window.localStorage.setItem("product", JSON.stringify(product))
        updateModal(m, product, handleAddClick, handleRemoveClick);
    });
}

if (localStorage.length > 0) {
    checkoutButton.classList.remove("disable-link");
}
else
{
    checkoutButton.classList.add("disable-link");
}


const handleCheckoutAddItem = async (e) => {
    let itemsInCart = LocalStorage.Get("shoppingCart");
    if (!itemsInCart) itemsInCart = []
    const product = await Api.getProduct(e.target.id)
    itemsInCart.push(product)
    LocalStorage.Set("shoppingCart", itemsInCart)
    updateCounter(itemsInCart)
    renderCheckoutContainer();
}

const handleCheckoutRemoveItem = async (e) => {
    let itemsInCart = LocalStorage.Get("shoppingCart");
    if (!itemsInCart) return
    const product = await Api.getProduct(e.target.id)
    let removed = false;
    const updatedCart = [];
    itemsInCart.forEach((p) => {
        if (!removed && p.id === product.id)
            removed = true;
        else updatedCart.push(p)

    })
    LocalStorage.Set("shoppingCart", updatedCart)
    updateCounter(updatedCart)
    renderCheckoutContainer();
}

function renderCheckoutContainer(){
    while (checkoutContainer.firstChild) {
        checkoutContainer.removeChild(checkoutContainer.lastChild);
    }
    const products = LocalStorage.Get("shoppingCart")
    const checkoutList = CheckoutList(products, handleCheckoutAddItem, handleCheckoutRemoveItem)
    checkoutContainer.appendChild(checkoutList)
}

if (checkoutContainer) {
    renderCheckoutContainer();
    
    const clearCartButton = document.getElementById('clearCartButton')
    
    clearCartButton.addEventListener('click', () => {
        LocalStorage.Set('shoppingCart', [])
        updateCounter([])
        renderCheckoutContainer();
    })

    makeOrderButton.onclick = function () {

        const products = LocalStorage.Get("order");

        (async () => {
            const rawResponse = await fetch('api/placeorder', {
                method: 'POST',
                headers: {'Content-Type': 'application/json'},
                body: JSON.stringify({"ShoppingCart": products})
            });
            if (rawResponse.redirected === true){
                const url = rawResponse.url;
                
                window.location.replace(rawResponse.url);
            }
            const response = rawResponse.statusText;
            if (response !== "OK") {
                alert("Something went wrong")
            }
        })();

    }
}
renderProductControls();
addCardEventListeners();
updateCategories();
updateCounter(LocalStorage.Get("shoppingCart"));