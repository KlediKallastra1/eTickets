const multiStepForm = document.querySelector("[data-multi-step]");
const formSteps = [...multiStepForm.querySelectorAll("[data-step]")];
let currentStep = formSteps.findIndex(step => {
    return step.classList.contains("active")
});

if (currentStep < 0) {
    currentStep = 0;
    showCurrentStep();
}

multiStepForm.addEventListener("click", e => {
    if (e.target.matches("[data-next]")) {
        currentStep++;
    }
    else if (e.target.matches("[data-previous]")) {
        currentStep--;
    }
    showCurrentStep();
});

formSteps.forEach(step => {
    step.addEventListener("animationend", e => {
        formSteps[currentStep].classList.remove("hide")
        e.target.classList.toggle("hide", !e.target.classList.contains("active"))
    });
});

function showCurrentStep() {
    formSteps.forEach((step, index) => {
        step.classList.toggle("active", index === currentStep);
    });
}