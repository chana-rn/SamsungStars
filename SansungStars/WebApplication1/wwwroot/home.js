
const userId = localStorage.getItem("userId");
if (!userId) {
    window.location.href = "login.html"; 
}

async function updateUser() {
    const userId = localStorage.getItem("userId");
    if (!userId) {
        window.location.href = "login.html";
    }
    const email = document.querySelector("#userName").value
    const password = document.querySelector("#password").value
    const firstName = document.querySelector("#firstName").value
    const lastName = document.querySelector("#lastName").value

    const user = { userId,email, password, firstName, lastName }
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
        if (strength === "לא ידוע") {
            alert("הכנס סיסמא");
            return;
        }
            

    }
    catch (error) {
        console.log(error)
    }
    try {
        const response = await fetch(`https://localhost:44325/api/Users/${userId}`, {
            method: 'PUT',
            body: JSON.stringify(user),
            headers: {
                'Content-Type': "application/json"
            }
        })
        if (!response.ok) {
            throw new Error("couldn't save data!")
        }
        getUser(userId);

    }
    catch (error) {
        console.log(error)
    }
    document.getElementById("successMessage").style.display = "block";
    setTimeout(() => {
        document.getElementById("successMessage").style.display = "none";
    }, 3000);
}
async function getUser(userId) {
    try {
        const response = await fetch(`https://localhost:44325/api/Users/${userId}`);
        if (!response.ok)
            throw new Error("User not found");

        const user = await response.json();
        document.getElementById("name").textContent ="Hi "+ user.firstName+" "+user.lastName;
        

    } catch (error) {
       alert("Error:", error);
    }
}

getUser(userId);
