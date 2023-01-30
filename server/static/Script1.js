// JavaScript source code
const let eHints = document.getElementById("hints");

function reset() {

}

function createAccount() {
    

}

function login() {
    document.getElementById("username").value
    presenceCheck(username, "Enter something pls :(")

    let password = document.getElementById("password").value
    presenceCheck(password, "Enter it dude")
    lengthCheck(password, 5, 10, "Between 5 and 10 pls")

}

function lengthCheck(input, minLength, maxLength, message) {
    if (input.length >= maxLength || input.length <= minLength) {
        eHints.innerText = message;
        throw message;
    }
}

function presenceCheck(input, message) {
    
    if (input == "") {
        eHints.innerText = message;
    } else {
        eHints.innerText = "";
    }
}