const textarea = document.getElementById("definition-value");
textarea.addEventListener("input", AdjustInputHeight);

export function AdjustInputHeight() {
    textarea.style.height = textarea.scrollHeight + "px";
}