const genreSelect = document.getElementById("genreSelect");
const genreIdInput = document.getElementById("GenreId");

genreSelect.addEventListener("change", function () {
    genreIdInput.value = this.value;
});