import {ProductControls} from "./productControls.js";

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
    console.log("hej")

    if (modal.classList.contains('loading')) modal.classList.remove('loading');

    const modalCard = document.querySelector(".modal-card");
    const modalImage = document.createElement('img')
    const modalImageContainer = document.createElement('div')
    const modalInfo = document.createElement('div')
    const title = document.createElement("h1")
    let productControls = document.createElement("div")
    productControls.id = product.id
    productControls = ProductControls(productControls, product.id, onAddClick, onRemoveClick, "update-modal");
    const description = document.createElement("p")
    const priceContainer = document.createElement('div');
    const volumeUnit = document.createElement("p");
    const inStock = document.createElement('p')
    const price = document.createElement("p");
    const modalFooter = document.createElement("div")
    const origin = document.createElement('p')
    const smallTextInfo = document.createElement('div')
    
    smallTextInfo.className = "card-product-flex-span"

    price.className = 'card-product-price modal-price'
    modalImage.className = 'modal-image'
    modalImage.src = "/images/" + product.image
    modalImageContainer.className = "modal-image"
    modalInfo.className = "modal-info"
    title.className = "modal-card-title"
    description.className = "modal-card-description"
    productControls.className = "modal-product-controls"
    priceContainer.className = "modal-price-container"

    modalFooter.className = "modal-card-footer"
    origin.className = "card-text card-text-small"
    title.textContent = product.name
    origin.textContent = product.origin
    description.textContent = product.description
    volumeUnit.textContent = product.volume +" " + product.unit
    volumeUnit.className = "card-text card-text-small"
    price.textContent = product.salesPrice + ":-"
    inStock.textContent = "Antal i lager: " + product.stock
    inStock.className = "card-text card-text-small"
    

    modalImageContainer.appendChild(modalImage)
    modalCard.appendChild(modalImage)
    modalInfo.appendChild(title)
    smallTextInfo.appendChild(origin)
    smallTextInfo.appendChild(volumeUnit)
    modalInfo.appendChild(smallTextInfo)
    modalInfo.appendChild(description)
    priceContainer.appendChild(price);
    priceContainer.appendChild(inStock);
    modalFooter.appendChild(priceContainer);
    modalFooter.appendChild(productControls)
    modalInfo.appendChild(modalFooter)
    modalCard.appendChild(modalInfo)
    
}

