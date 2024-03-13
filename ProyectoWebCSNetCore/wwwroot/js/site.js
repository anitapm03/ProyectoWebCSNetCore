// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/************ JS **************

// carousel comentarios post / blog
const commentsCarousel = document.querySelector(".comments-container");
const nextBtn = document.querySelector(".next-btn");
const prevBtn = document.querySelector(".prev-btn");

const carouselItems = document.querySelectorAll(".swiper-slide");
let currentIndex = 0;
const itemWidth = carouselItems[0].offsetWidth - parseInt(getComputedStyle(carouselItems[0]).marginRight);
const totalItems = carouselItems.length;

//-- next
nextBtn.addEventListener("click", function () {

    currentIndex = (currentIndex + 1) % totalItems;
    //currentIndex++;

    console.log(carouselItems[currentIndex]);

    commentsCarousel.style.transform = `translate3d(${-currentIndex * itemWidth}px,0px,0px)`;
    commentsCarousel.style.transitionDuration = '300ms';

    carouselItems[currentIndex].style.Width = '331.5px';
    carouselItems[currentIndex].style.marginRight = '61px';
});

//-- previous
prevBtn.addEventListener("click", function () {
    currentIndex = (currentIndex - 1 + totalItems) % totalItems;
    //currentIndex--;

    console.log(carouselItems[currentIndex]);

    commentsCarousel.style.transform = `translate3d(${-currentIndex * itemWidth}px,0px,0px)`;
    commentsCarousel.style.transitionDuration = '300ms';

    carouselItems[currentIndex].style.Width = '331.5px';
    carouselItems[currentIndex].style.marginRight = '61px';
});
*/

//segundo intento

var multipleCardCarousel = document.querySelector(
    "#carouselExampleControls"
);
if (window.matchMedia("(min-width: 768px)").matches) {
    var carousel = new bootstrap.Carousel(multipleCardCarousel, {
        interval: false,
    });
    var carouselWidth = $(".carousel-inner")[0].scrollWidth;
    var cardWidth = $(".carousel-item").width();
    var scrollPosition = 0;
    $("#carouselExampleControls .carousel-control-next").on("click", function () {
        if (scrollPosition < carouselWidth - cardWidth * 4) {
            scrollPosition += cardWidth;
            $("#carouselExampleControls .carousel-inner").animate(
                { scrollLeft: scrollPosition },
                600
            );
        }
    });
    $("#carouselExampleControls .carousel-control-prev").on("click", function () {
        if (scrollPosition > 0) {
            scrollPosition -= cardWidth;
            $("#carouselExampleControls .carousel-inner").animate(
                { scrollLeft: scrollPosition },
                600
            );
        }
    });
} else {
    $(multipleCardCarousel).addClass("slide");
}