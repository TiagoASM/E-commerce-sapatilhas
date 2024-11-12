let slideIndex = 0;
showSlides();

function showSlides() {
    let slides = document.getElementsByClassName("image-container");

    // Esconde todas as imagens
    for (let i = 0; i < slides.length; i++) {
        slides[i].style.opacity = "0";
    }

    slideIndex++;
    if (slideIndex > slides.length) { slideIndex = 1; }

    // Exibe a imagem atual
    slides[slideIndex - 1].style.opacity = "1";

    // Muda a imagem a cada 3 segundos
    setTimeout(showSlides, 5000);
}