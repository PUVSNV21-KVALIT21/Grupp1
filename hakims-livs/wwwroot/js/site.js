import { createModal, updateModal } from "./components/modal.js";

alert('hi')
localStorage.setItem("productId", 1)

console.log(localStorage.getItem("productId"))


const main = document.getElementById("site")
const modalContainer = document.getElementById("modal-container")

const productCards = document.querySelectorAll(".card-product");
productCards.forEach(card => {
    card.addEventListener('click', (event) => {
        handleProductClick(event, card.id)
    })
})



async function fetchProduct(id) {
    const response = await fetch('/api/Products/' + id);
    return await response.json();
}

const handleModalClick = (e) => {

    console.log(e)
    e.preventDefault()
    //do nothing if the click happened within the card
    if (e.target.className === ('modal-card')){
        return
    }

    //else remove modal & background blur
    modalContainer.removeChild(e.target)
    main.className = "";
}

const handleProductClick = (e, id) => {
    main.className = "blurred";
    const m = createModal(handleModalClick);
    modalContainer.appendChild(m)
    fetchProduct(id).then(product => {
        window.localStorage.setItem("product", JSON.stringify(product))
        console.log(localStorage.getItem("product"))
        updateModal(m, product);
    });
}