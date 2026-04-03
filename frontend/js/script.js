console.log("hello world");

const myName = "Jakub";
const h1 = document.querySelector(".heading-primary");

console.log(myName);
console.log(h1);

// h1.addEventListener("click", function () {
//    h1.textContent = myName;
//    h1.style.backgroundColor = "red";
//    h1.style.padding = "5rem";
// });

///////////////////////////////////////////////////////////
//Set current year for copyright
const yearEl = document.querySelector(".year");
const currYear = new Date().getFullYear();
yearEl.textContent = currYear;

///////////////////////////////////////////////////////////
//Make mobile navigation work
const bntNavEl = document.querySelector(".btn-mobile-nav");
const headerEl = document.querySelector(".header");

bntNavEl.addEventListener("click", function () {
   headerEl.classList.toggle("nav-open");
});

///////////////////////////////////////////////////////////
//Smooth scrolling
const allLinks = document.querySelectorAll("a:link");
console.log(allLinks);
allLinks.forEach(function (link) {
   link.addEventListener("click", function (e) {
      const href = link.getAttribute("href");

      // Only prevent default behavior if the link is an internal anchor (#)
      if (href && href.startsWith("#")) {
         e.preventDefault();

         //Scroll back to top
         if (href === "#") {
            window.scrollTo({
               top: 0,
               behavior: "smooth",
            });
         }

         //Scroll to other links
         if (href !== "#" && href.startsWith("#")) {
            const sectionEl = document.querySelector(href);
            if (sectionEl) sectionEl.scrollIntoView({ behavior: "smooth" });
         }

         //Close mobile navigation
         if (link.classList.contains("main-nav-link")) {
            headerEl.classList.toggle("nav-open");
         }
      }
   });
});
///////////////////////////////////////////////////////////
//Sticky navigation
const sectionHeroEl = document.querySelector(".section-hero");

const observer = new IntersectionObserver(
   function (entries) {
      const entry = entries[0];
      // console.log(entry);
      if (entry.isIntersecting === false) {
         document.body.classList.add("sticky-nav");
      }
      if (entry.isIntersecting === true) {
         document.body.classList.remove("sticky-nav");
      }
   },

   {
      //In the viewport
      //  null = viewport
      root: null,
      threshold: 0,
      rootMargin: "-62px",
   },
);
observer.observe(sectionHeroEl);

///////////////////////////////////////////////////////////
// Fixing flexbox gap property missing in some Safari versions
function checkFlexGap() {
   var flex = document.createElement("div");
   flex.style.display = "flex";
   flex.style.flexDirection = "column";
   flex.style.rowGap = "1px";

   flex.appendChild(document.createElement("div"));
   flex.appendChild(document.createElement("div"));

   document.body.appendChild(flex);
   var isSupported = flex.scrollHeight === 1;
   flex.parentNode.removeChild(flex);
   console.log(isSupported);

   if (!isSupported) document.body.classList.add("no-flexbox-gap");
}
checkFlexGap();

// https://unpkg.com/smoothscroll-polyfill@0.4.4/dist/smoothscroll.min.js
