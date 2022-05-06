import {createModal, updateModal, updateModalButtons} from "./components/modal.js";
import { Api } from "./components/api.js";
import LocalStorage from "./components/localStorage.js";

const cartCounter = document.querySelector('.shoppingCart-counter')
const main = document.getElementById("site")
const modalContainer = document.getElementById("modal-container")
const checkoutButton = document.getElementById("checkoutButton")

const productCards = document.querySelectorAll(".card-product");
productCards.forEach(card => {
    card.addEventListener('click', (event) => {
        handleProductClick(event, card.id)
    })
})

const updateCounter = (itemsInCart) => {
    
    if (!itemsInCart) return
    
    let value = 0;
    
    itemsInCart.forEach((item) => {
        const price = item.salesPrice;
        if (price) value = value + price
    })
    
    cartCounter.textContent = value > 0 ? value + "kr" : "";
}



const handleModalClick = (e) => {

    e.preventDefault()
    //do nothing if the click happened within the card
    if (e.target.className === ('modal-card')){
        return
    }

    //else remove modal & background blur
    modalContainer.removeChild(e.target)
    main.className = "";
}

const handleAddClick = async (e) => {
    let itemsInCart = LocalStorage.Get("shoppingCart");
    if (!itemsInCart) itemsInCart = []
    const product = await Api.getProduct(e.target.id)
    itemsInCart.push(product)
    LocalStorage.Set("shoppingCart", itemsInCart)
    const modalControllers = document.querySelector('.modal-product-controls')
    updateModalButtons(modalControllers, product, handleAddClick, handleRemoveClick)
    updateCounter(itemsInCart)
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
    const modalControllers = document.querySelector('.modal-product-controls')
    updateModalButtons(modalControllers, product, handleAddClick, handleRemoveClick)
    updateCounter(updatedCart)
}



const handleProductClick = (e, id) => {
    main.className = "blurred";
    const m = createModal(handleModalClick);
    modalContainer.appendChild(m)
    Api.getProduct(id).then(product => {
        window.localStorage.setItem("product", JSON.stringify(product))
        updateModal(m, product, handleAddClick, handleRemoveClick);
    });
}

if (localStorage.length > 0) {
    checkoutButton.textContent = "Till kassan:";
    checkoutButton.classList.remove("disable-link");
}
else
{
    checkoutButton.textContent = "Till kassan";
    checkoutButton.classList.add("disable-link");
}

updateCounter(LocalStorage.Get("shoppingCart"));