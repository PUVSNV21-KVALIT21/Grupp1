import LocalStorage from "./localStorage.js";

export const  CheckoutList = (products, handleAddClick, handleRemoveClick) => {
    const checkOutItems = [];
    const ids = [];
    
    const checkoutList = document.createElement('div');
    checkoutList.className = "checkoutList";
    
    products.forEach(product => {
        if (ids.includes(product.id)){
            let item = checkOutItems.find(i => i.productId === product.id);
            item.quantity = item.quantity +1;
        } else {
            const item = {
                "productId" : product.id ,
                "quantity" : 1
            }
            checkOutItems.push(item)
            ids.push(product.id)
        }
    })
    
    LocalStorage.Set('order', checkOutItems)
    
    let totalPrice = 0;
    checkOutItems.forEach(item => {
        const product = products.filter(p => (p.id === item.productId))[0];
        totalPrice = totalPrice + product.salesPrice;
        const itemElement = document.createElement('div');
        itemElement.className = "checkoutItem";
        const details = document.createElement('div')
        const name = document.createElement('b')
        name.textContent = product.name;
        details.appendChild(name)

        const price = document.createElement('p')
        price.textContent = "Styckpris: " + product.salesPrice + " kr";
        details.appendChild(price)

        const quantity = document.createElement('p')
        quantity.textContent = "Antal: " + item.quantity + " st";
        details.appendChild(quantity)
        
        
        const imageDiv = document.createElement('div')
        imageDiv.className = "checkoutList-image-container"
        const image = document.createElement('img')
        image.className = 'checkoutList-image'
        image.src = "/images/" + product.image
        image.className = "checkoutList-image"
        imageDiv.appendChild(image)
        
        const controls = document.createElement('div')
        controls.className = "checkoutList-controls"
        
        const addButton = document.createElement('button');
        addButton.className = "btn btn-primary btn-sm btn-checkout"
        addButton.id = item.productId;
        const addIcon = document.createElement('i')
        addIcon.id = item.productId
        addIcon.className = "fa fa-plus"
        addButton.appendChild(addIcon);
        addButton.addEventListener('click', handleAddClick)
        
        const removeButton = document.createElement('button');
        const removeIcon = document.createElement('i')
        removeIcon.className = "fa fa-minus"
        removeIcon.id = item.productId
        removeButton.appendChild(removeIcon);
        removeButton.className = "btn btn-primary btn-sm btn-checkout"
        removeButton.id = item.productId;
        removeButton.addEventListener('click', handleRemoveClick)
        
        itemElement.appendChild(imageDiv)
        itemElement.appendChild(details)
        controls.appendChild(addButton)
        controls.appendChild(removeButton)
        itemElement.appendChild(controls)
        checkoutList.appendChild(itemElement)
    })

    return checkoutList;
}
    