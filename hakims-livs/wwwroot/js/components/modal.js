import {LocalStorage} from "./localStorage.js";

export const createModal = (handleClick) => {

    const modal = document.createElement('div')
    const modalCard = document.createElement('div')

    modalCard.className = 'modal-card'
    modal.classList.add("loading");
    modal.className = 'modal-background'
    modal.addEventListener('click', handleClick)
    modal.appendChild(modalCard)

    return modal
}

export function updateModal(modal, product, onAddClick, onRemoveClick){

    if (modal.classList.contains('loading')) modal.classList.remove('loading');

    const modalCard = document.querySelector(".modal-card");
    const modalImage = document.createElement('img')
    const modalImageContainer = document.createElement('div')
    const modalInfo = document.createElement('div')
    const title = document.createElement("h1")
    let productControls = document.createElement("div")
    productControls = updateModalButtons(productControls, product, onAddClick, onRemoveClick);
    const description = document.createElement("p")
    const priceContainer = document.createElement('div');
    const volumeUnit = document.createElement("p");
    const price = document.createElement("h2");
    const modalFooter = document.createElement("div")
    const origin = document.createElement('p')

    price.className = 'modal-price'
    modalImage.className = 'modal-image'
    modalImage.src = "/images/" + product.image
    modalImageContainer.className = "modal-image"
    modalInfo.className = "modal-info"
    title.className = "modal-card-title"
    description.className = "modal-card-description"
    productControls.className = "modal-product-controls"
    priceContainer.className = "modal-price-container"

    modalFooter.className = "modal-card-footer"
    origin.className = "modal-origin"

    title.textContent = product.name
    origin.textContent = product.origin
    description.textContent = product.description
    volumeUnit.textContent = product.volume +" " + product.unit
    price.textContent = product.salesPrice + "kr /"
    

    modalImageContainer.appendChild(modalImage)
    modalCard.appendChild(modalImage)
    modalInfo.appendChild(title)
    modalInfo.appendChild(origin)
    modalInfo.appendChild(description)
    priceContainer.appendChild(price);
    priceContainer.appendChild(volumeUnit);
    modalFooter.appendChild(priceContainer);
    modalFooter.appendChild(productControls)
    modalInfo.appendChild(modalFooter)
    modalCard.appendChild(modalInfo)



}

export const updateModalButtons = (parent, product, onAddClick, onRemoveClick) => {
    while (parent.firstChild) {
        parent.removeChild(parent.lastChild);
    }
    const buyButton = document.createElement("button")
    const removeButton = document.createElement("button")
    buyButton.addEventListener("click", onAddClick)
    removeButton.addEventListener("click", onRemoveClick)
    buyButton.className = "btn btn-primary btn-product btn-product-add"
    buyButton.id = product.id
    removeButton.className = "btn btn-primary btn-product btn-product-remove"
    removeButton.id = product.id
    const cart = LocalStorage.Get("shoppingCart")
    let cartContainsProduct;
    cart.forEach((p) => {
        if (p.id === product.id){
            cartContainsProduct = true;
        }
    })
    console.log(cartContainsProduct)
    console.log(cart)
    if (cartContainsProduct) {
        console.log("IN CART")
        parent.appendChild(removeButton);
        parent.appendChild(buyButton)
        buyButton.textContent = "+"
        removeButton.textContent = "-"
    } else {
        parent.appendChild(buyButton)
        buyButton.textContent = "Köp"
    }
    return parent;
}