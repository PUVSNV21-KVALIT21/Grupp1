import {LocalStorage} from "./localStorage.js";

export const ProductControls = (parent, productId, onAddClick, onRemoveClick) => {
    parent.id = productId
    productId = productId.toString()
    while (parent.firstChild) {
        parent.removeChild(parent.lastChild);
    }
    const buyButton = document.createElement("button")
    const removeButton = document.createElement("button")

    buyButton.addEventListener("click", onAddClick)

    removeButton.addEventListener("click", onRemoveClick)
    buyButton.className = "btn btn-primary btn-product btn-product-add"
    buyButton.id = productId
    removeButton.className = "btn btn-primary btn-product btn-product-remove"
    removeButton.id = productId
    const cart = LocalStorage.Get("shoppingCart")
    let cartContainsProduct = false
    let productCounter = 0;
    cart.forEach((p) => {
        if (parseInt(p.id) == productId){
            productCounter += 1;
            cartContainsProduct = true;
        }
    })
    if (cartContainsProduct) {
        
        const productCount = document.createElement('span')
        productCount.textContent = productCounter
        productCount.className = "product-count"

        const addIcon = document.createElement('i')
        addIcon.className = "fa fa-plus"
        addIcon.classList.add("control")
        addIcon.id = productId
        buyButton.appendChild(addIcon);

        const removeIcon = document.createElement('i')
        removeIcon.className = "fa fa-minus"
        removeIcon.classList.add("control")
        removeIcon.id = productId
        removeButton.appendChild(removeIcon);

        parent.appendChild(removeButton);
        parent.appendChild(productCount)
        parent.appendChild(buyButton)

    } else {
        buyButton.classList.add('btn-block')
        parent.appendChild(buyButton)
        buyButton.textContent = "Köp"
    }
    return parent;
}