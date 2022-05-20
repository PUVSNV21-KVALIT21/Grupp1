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
const printButton = document.getElementById("printButton")

if (printButton){
    printButton.addEventListener('click', () => {

        const printArea = document.getElementById('pickingListPrintArea').innerHTML;
        const a = window.open('', '', 'height=1920, width=1080');
        a.document.write('<html><header>');
        a.document.write('<h1>Plocklista</h1>')
        a.document.write('</header>');
        a.document.write('<body>');
        a.document.write(printArea);
        a.document.write('</body></html>');
        a.document.close();
        a.print();
    })
}



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


const categoriesContainer = document.querySelector(".categories-container")
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
    value = value.toFixed(2)

    let formattedValue = value.replace('.', ',')
    if (formattedValue[formattedValue.length-1] === '0' && formattedValue[value.length-2] === '0'){
        formattedValue = formattedValue.split(",")[0]
        
        
    }
    
    cartCounter.textContent = value > 0 ? formattedValue + " kr" : "";
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
    const products = LocalStorage.Get("shoppingCart")
    if (products.length === 0){
        window.location.replace("/index");
    }
    while (checkoutContainer.firstChild) {
        checkoutContainer.removeChild(checkoutContainer.lastChild);
    }

    const checkoutList = CheckoutList(products, handleCheckoutAddItem, handleCheckoutRemoveItem)
    checkoutContainer.appendChild(checkoutList)
}

function checkCard() {
    const cardNumber = document.getElementById("cardNumberField")
    const cardHolder = document.getElementById("cardHolderField")
    const cardExpire = document.getElementById("cardExpireDateField")
    const cardCVC = document.getElementById("cardCVCField")

    var valid = true;
    if (document.getElementById("paymentOption").value === "card") {
        if (!cardNumber.value) {
            alert("Du måste fylla i ett kortnummer")
            valid = false;
        }
        else if (cardNumber.value.length != 16) {
            alert("Du har fyllt i ett för kort kortnummer")
            valid = false;
        }

        if (!cardHolder.value) {
            alert("Du måste fylla i kortinnehavarens namn")
            valid = false;
        }
        if (!cardExpire.value) {
            alert("Du måste fylla i förfallodatum")
            valid = false;
        }
        if (!cardCVC.value) {
            alert("Du måste fylla i CVC")
            valid = false;
        }
        else if (cardCVC.value.length !== 3) {
            alert("Du har fyllt i ett för kort CVC nummer")
            valid = false;
        }
        return valid;
    }
    else {
        return true;
    }
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
        if (checkCard()) {
            const products = LocalStorage.Get("order");

            (async () => {
                const rawResponse = await fetch('api/placeorder', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ "ShoppingCart": products })
                });
                if (rawResponse.redirected === true) {
                    const url = rawResponse.url;

                    window.location.replace(rawResponse.url);
                }
                /*const response = rawResponse.statusText;*/
                else {
                    alert("Something went wrong")
                }
            })();
        }
    }
}

//popup label
const tooltip = document.getElementById('icon-tooltip');

//make popup follow mouse movement
window.addEventListener('mousemove', function(e) {
    const left = e.pageX;
    const top = e.pageY - 50;
    tooltip.style.left = left + 'px';
    tooltip.style.transform = "translate(-50%, 0)"
    tooltip.style.top = top + 'px';
});

const allIcons = document.querySelectorAll(".card-product-icon");
//show popup label on mouse enter, and add text stored in rect
allIcons.forEach(r => r.addEventListener('mouseenter', function(e){
    tooltip.classList.add("visible");
    tooltip.textContent = e.target.id;
}))

allIcons.forEach(r => r.addEventListener('mouseout', function(e){
    tooltip.classList.remove("visible");
}))



renderProductControls();
addCardEventListeners();
updateCategories();
updateCounter(LocalStorage.Get("shoppingCart"));
