// Wait until the DOM is fully loaded
document.addEventListener("DOMContentLoaded", () => {
    // Get references to the password input and strength bar
    const passwordInput = document.getElementById("Password");
    const strengthBar = document.getElementById("passwordStrengthBar");

    // Get references to the password rule elements
    const rules = {
        length: document.getElementById("ruleLength"),
        upper: document.getElementById("ruleUpper"),
        lower: document.getElementById("ruleLower"),
        number: document.getElementById("ruleNumber"),
        special: document.getElementById("ruleSpecial")
    };

    // Listen for input changes on the password field
    passwordInput.addEventListener("input", () => {
        const val = passwordInput.value;
        let score = 0;

        // Check length requirement
        if (val.length >= 8) {
            rules.length.textContent = "✅ At least 8 characters";
            rules.length.classList.replace("text-danger", "text-success");
            score++;
        } else {
            rules.length.textContent = "❌ At least 8 characters";
            rules.length.classList.replace("text-success", "text-danger");
        }

        // Check for uppercase letter
        if (/[A-Z]/.test(val)) {
            rules.upper.textContent = "✅ At least one uppercase letter";
            rules.upper.classList.replace("text-danger", "text-success");
            score++;
        } else {
            rules.upper.textContent = "❌ At least one uppercase letter";
            rules.upper.classList.replace("text-success", "text-danger");
        }

        // Check for lowercase letter
        if (/[a-z]/.test(val)) {
            rules.lower.textContent = "✅ At least one lowercase letter";
            rules.lower.classList.replace("text-danger", "text-success");
            score++;
        } else {
            rules.lower.textContent = "❌ At least one lowercase letter";
            rules.lower.classList.replace("text-success", "text-danger");
        }

        // Check for number
        if (/[0-9]/.test(val)) {
            rules.number.textContent = "✅ At least one number";
            rules.number.classList.replace("text-danger", "text-success");
            score++;
        } else {
            rules.number.textContent = "❌ At least one number";
            rules.number.classList.replace("text-success", "text-danger");
        }

        // Check for special character
        if (/[!@#$%^&*()_\-+=<>?]/.test(val)) {
            rules.special.textContent = "✅ At least one special character";
            rules.special.classList.replace("text-danger", "text-success");
            score++;
        } else {
            rules.special.textContent = "❌ At least one special character";
            rules.special.classList.replace("text-success", "text-danger");
        }

        // Update the visual strength bar
        const percentage = (score / 5) * 100;
        strengthBar.style.width = percentage + "%";
        strengthBar.className = "progress-bar " + (percentage < 40 ? "bg-danger" : percentage < 80 ? "bg-warning" : "bg-success");
    });
});