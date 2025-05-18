function showRegister() {
    document.getElementById("login-form").style.display = "none";
    document.getElementById("register-form").style.display = "block";
    document.getElementById("form-title").innerText = "צור חשבון חדש";
}

function showLogin() {
    document.getElementById("register-form").style.display = "none";
    document.getElementById("login-form").style.display = "block";
    document.getElementById("form-title").innerText = "ברוך הבא!";
}

async function checkPassword() {

    const password = document.querySelector("#newPassword").value
    try {
        const res = await fetch("https://localhost:44325/api/Users/checkPassword", {
            method: 'POST',
            body: JSON.stringify(password),
            headers: {
                'Content-Type': "application/json"
            }
        })
        const strength = await res.text();
        if (strength === "חלשה מאד" || strength === "חלשה") {
            alert("יש להזין סיסמא חזקה יותר");
            return;
        }
        alert("great!")

    }
    catch (error) {
        console.log(error)
    }
}


async function register() {
    const email = document.querySelector("#newUserName").value
    const password = document.querySelector("#newPassword").value
    const firstName = document.querySelector("#firstName").value
    const lastName = document.querySelector("#lastName").value

    const user = { email, password, firstName, lastName }
    try {
        const res = await fetch("https://localhost:44325/api/Users/checkPassword", {
            method: 'POST',
            body: JSON.stringify(user.password),
            headers: {
                'Content-Type': "application/json"
            }
        })
        const strength = await res.text();
        if (strength === "חלשה מאד" || strength === "חלשה") {
            alert("יש להזין סיסמא חזקה יותר");
            return;
        }

    }
    catch (error) {
        console.log(error)
    }
    try {
        const response = await fetch("https://localhost:44325/api/Users", {
            method: 'POST',
            body: JSON.stringify(user),
            headers: {
                'Content-Type': "application/json"
            }
        })
        if (!response.ok) {
            throw new Error("couldn't save data!")
        }
        const data = await response.json();
        console.log(data);
        localStorage.setItem("id", data.id);
        window.location.href = "home.html"

    }
    catch (error) {
        console.log(error)
    }

}

async function login() {
    const email = document.querySelector("#userName").value
    const password = document.querySelector("#password").value

    const user = { email, password }

    try {
        const response = await fetch("https://localhost:44325/api/Users/login", {
            method: 'POST',
            body: JSON.stringify(user),
            headers: {
                'Content-Type': "application/json"
            }
        })
        if (!response.ok) {
            throw new Error("Incorrect email or password");
        }
        const data = await response.json();
        console.log(data)
        localStorage.setItem("id", data.id);
        window.location.href = "home.html"
       

    }
    catch (error) {
        alert(error)
    }
}