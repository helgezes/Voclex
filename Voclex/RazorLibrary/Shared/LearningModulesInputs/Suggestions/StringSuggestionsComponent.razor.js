const parentElement = document.body;

parentElement.addEventListener("input", (event) => {
    if (event.target.classList.contains("adjustable-height")) {
        AdjustAllInputHeights();
    }
});

export function AdjustAllInputHeights() {
    const textareas = document.querySelectorAll(".adjustable-height");
    textareas.forEach((textarea) => {
        textarea.style.height = textarea.scrollHeight + "px";
    });
}
