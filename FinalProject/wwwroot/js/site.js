var themeSwitcher = document.querySelector('#theme-switcher')
var themeLinks = document.querySelectorAll('#theme-switch')  // Select all anchor elements within the theme-switch

var currentTheme = localStorage.getItem('theme') ? localStorage.getItem('theme') : document.documentElement.getAttribute('data-bs-theme')

var setTheme = theme => {
    document.documentElement.setAttribute('data-bs-theme', theme)
    localStorage.setItem('theme', theme)
};

setTheme(currentTheme)

themeLinks.forEach(link => {

    if (localStorage.getItem('theme') === link.getAttribute('data-theme')) {
        link.classList.add('active')
    } else {
        link.classList.remove('active')
    }

    link.addEventListener('click', e => {  // Add event listener to each link, not themeLinks as a whole
        e.preventDefault();

        themeLinks.forEach(l => {
            l.classList.remove('active')
        });

        var theme = link.getAttribute('data-theme')
        link.classList.add('active')  // Add active class to the clicked link
        //console.log(theme)
        setTheme(theme)
    })

});
