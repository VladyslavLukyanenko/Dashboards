(function () {
  var slideout = new Slideout({
    'panel': document.querySelector('.Root'),
    'menu': document.querySelector('.MobileMenu'),
    'padding': 256,
    'tolerance': 70
  });

  var mainNav = document.querySelector(".MainNav");
  slideout.on("open", tryToggleSlideout);
  slideout.on("close", tryToggleSlideout);
  slideout.on('translatestart', function() {
    mainNav.classList.add("is-opening");
  });
// Toggle button
  document.querySelector('.MobileMenuToggle').addEventListener('click', function () {
    slideout.toggle();
    tryToggleSlideout();
  });


  new SmoothScroll('a[href*="#"]', {
    speed: 500,
    header: '[data-scroll-header]'
  });

  window.addEventListener("scroll", function () {
    mainNav.classList.toggle("is-scrolled", !!window.scrollY);
  });

  function tryToggleSlideout() {
    mainNav.classList.toggle("is-menuOpened", slideout.isOpen());
    mainNav.classList.remove("is-opening");
  }

  window.addEventListener("load", function () {
    AOS.init();
  });
})();

