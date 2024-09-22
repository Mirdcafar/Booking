const swiper = new Swiper('.swiper', {

    loop: true,
    grabCursor: true,
    spaceBetween: 30,

    pagination: {
        el: '.swiper-pagination',
    },
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },
    breakpoints: {
        320: {
            slidesPerView: 2,
        },
        720: {
            slidesPerView: 4,
        },
        1024: {
            slidesPerView: 6,
        }
    }

});

const swipers = new Swiper('.swipers', {

    loop: true,
    grabCursor: true,
    spaceBetween: 30,

    pagination: {
        el: '.swiper-pagination',
    },
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },
    breakpoints: {
        320: {
            slidesPerView: 1,
        },
        720: {
            slidesPerView: 2,
        },
        1024: {
            slidesPerView: 4,
        }
    }

});


