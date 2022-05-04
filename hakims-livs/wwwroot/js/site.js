const main = document.getElementById("site")
const modalContainer = document.getElementById("modal-container")

const productCards = document.querySelectorAll(".card-product");
productCards.forEach(card => {
    card.addEventListener('click', (event) => {
        handleProductClick(event, card.id)
    })
})

const handleProductClick = (e, id) => {
    main.className = "blurred";
    const m = createModal();
    modalContainer.appendChild(m)
    fetchProduct(id).then(product => {
        updateModal(m, product);
    });
}

async function fetchProduct(id) {
    const response = await fetch('/api/Products/' + id);
    return await response.json();
}

function updateModal(modal, product){

    if (modal.classList.contains('loading')) modal.classList.remove('loading');

    const modalCard = document.querySelector(".modal-card");
    const modalImage = document.createElement('img')
    const modalImageContainer = document.createElement('div')
    const modalInfo = document.createElement('div')
    const title = document.createElement("h1")
    const productControls = document.createElement("div")
    const buyButton = document.createElement("button")
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
    buyButton.className = "btn btn-primary btn-product"
    modalFooter.className = "modal-card-footer"
    origin.className = "modal-origin"

    modalImageContainer.appendChild(modalImage)
    productControls.appendChild(buyButton)
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


    title.textContent = product.name
    origin.textContent = product.origin
    description.textContent = product.description
    volumeUnit.textContent = product.volume +" " + product.unit
    price.textContent = product.salesPrice + "kr /"
    buyButton.textContent = "Köp"
}

const handleModalClick = (e) => {

    console.log(e)
    e.preventDefault()
    //do nothing if the click happened within the image
    if (e.target.className === ('modal-card')){
        return
    }

    //else remove modal & background blur
    modalContainer.removeChild(e.target)
    main.className = "";
}

const createModal = () => {

    const modal = document.createElement('div')
    const modalCard = document.createElement('div')

    modalCard.className = 'modal-card'
    modal.classList.add("loading");
    modal.className = 'modal-background'
    modal.addEventListener('click', handleModalClick)
    modal.appendChild(modalCard)

    return modal
}