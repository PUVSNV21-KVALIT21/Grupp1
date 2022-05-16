const cardPayment = document.getElementById("cardPaymentContainer");
const swishPayment = document.getElementById("swishPaymentContainer");
const invoicePayment = document.getElementById("invoicePaymentContainer");

document.getElementById("paymentOption").addEventListener("change", function () {
    if (this.value == "card") {
        cardPayment.removeAttribute("hidden")
        swishPayment.setAttribute("hidden", true)
        invoicePayment.setAttribute("hidden", true)
    }
    else if (this.value == "swish") {
        swishPayment.removeAttribute("hidden")
        cardPayment.setAttribute("hidden", true)
        invoicePayment.setAttribute("hidden", true)
    }
    else if (this.value == "invoice") {
        invoicePayment.removeAttribute("hidden")
        swishPayment.setAttribute("hidden", true)
        cardPayment.setAttribute("hidden", true)
    }
})