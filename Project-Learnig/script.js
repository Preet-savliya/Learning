// dom element for navlinks //
const navLinks = document.querySelectorAll('#navLinks a');
// dom elements for sidebar //
const sidebar = document.getElementById('sidebar');
// dom element for hamburger //
const hamburger = document.getElementById('hamburger');


// for mapping object for sidebar menu //

const sidebarData = {
    newlot: [
        { label: 'Dashboard', url: '#' },
        { label: 'Create Lot', url: '#' },
        { label: 'Lot History', url: '#' }
    ],
    stock: [
        { label: 'Dashboard', url: '#' },
        { label: 'Current Stock', url: '#' },
        { label: 'Stock History', url: '#' }
    ],
    inquiry: [
        { label: 'Dashboard', url: '#' },
        { label: 'New Inquiry', url: '#' },
        { label: 'Inquiry History', url: '#' }
    ],
    average: [
        { label: 'Dashboard', url: 'average-dashboard.html' },
        { label: 'New Lot Average', url: 'newlot-average.html' },
        { label: 'ReAssort Average', url: '#' },
        { label: 'Purchase Average', url: '#' },
        { label: 'Repaired Average', url: '#' },
        { label: 'Rejection Average', url: '#' }
    ],
    lotbatch: [
        { label: 'Dashboard', url: '#' },
        { label: 'Batch Details', url: '#' },
        { label: 'Batch History', url: '#' }
    ]
};

// ================= DROPDOWN FUNCTION =================
function toggleDropdown() {
    // dom element for userdropdown //
    const dropdown = document.getElementById('userDropdown');
    // checks conditions that for show and hide
    if (dropdown.style.display === 'flex') {
        dropdown.style.display = 'none';
    } else {
        dropdown.style.display = 'flex';
    }
}

// ================= HAMBURGER/SIDEBAR FUNCTION =================
// default side bar hidden 
function toggleSidebar() {
    sidebar.classList.toggle('hidden');
}

// active state for main navigation
// when menu is clicked then the menu should stay in active state //
function setActiveMenu(link) {
    navLinks.forEach(l => l.classList.remove('active'));
    link.classList.add('active');
}

// Close dropdown if user clicks outside of it
window.addEventListener('click', function (e) {
    // dom element selecting the userdropdown //
    const dropdown = document.getElementById('userDropdown');
    // dom element for the user //
    const user = document.querySelector('.user');
    // checks condition if user is clicking outside dropdown targeting location of the user //
    if (dropdown && user && !user.contains(e.target)) {
        dropdown.style.display = 'none';
    }
});

// NAV LINK CLICK HANDLE
// creating loop for main menu //
navLinks.forEach(link => {
    // added click event for every link //
    link.addEventListener('click', function (e) {
        // after click it takes the data-sidebar value from the link //
        const sidebarKey = this.getAttribute('data-sidebar');

        // check that it has data-sidebar or not then its check the key sideBardata is there or not //
        if (sidebarKey && sidebarData[sidebarKey]) {
            // this is used for stoping default page navigation //
            e.preventDefault();
            // taking data from the selected sidebar data //
            const data = sidebarData[sidebarKey];

            // it active main menu link //
            setActiveMenu(this);

            // this creates sidebar links dynamically //
            sidebar.innerHTML = data
                .map(item => `<a href="${item.url}">${item.label}</a>`)
                .join('');

            // it showing sidebar and hamburger when the navlink has sidebardata and sidebarkey //
            sidebar.classList.remove('hidden');
            hamburger.classList.remove('hidden');

      // checks if there is a valid url then it automatically opens the first url from the sidebar otherwise it gives # instead of url (e.g., http://127.0.0.1:5500/index.html# ) // 
            if (data.length > 0 && data[0].url !== '#') {
                window.location.href = data[0].url;
            }

            // and if sidebar is not there then it hides sidebar and it actives menu which is selected //
        } else {
            setActiveMenu(this);
            sidebar.classList.add('hidden');
        }
    });
});

// WINDOW LOAD LOGIC
window.addEventListener('DOMContentLoaded', () => {
    // it indentify current page filename //
    const currentPath = window.location.pathname.split('/').pop();

    let isSubmenuPage = false;
// creating loop for every sidebar group //
    for (const key in sidebarData) {
        // it takes submenuitems //
        const subItems = sidebarData[key];
        // checkes if it is in current page submenu or not //
        const match = subItems.find(item => item.url === currentPath);
        // if it is there is submenu //
        if (match) {
            // then that page is sidebar menu page //
            isSubmenuPage = true;
            // it finds main menu link //
            const parentLink = document.querySelector(`#navLinks a[data-sidebar="${key}"]`);
            // it actives main menu link //
            if (parentLink) setActiveMenu(parentLink);

            // it creates sidebar menu and it actives current submenu item //
            sidebar.innerHTML = subItems
                .map(item => {
                    const activeClass = (item.url === currentPath) ? 'active' : '';
                    return `<a href="${item.url}" class="${activeClass}">${item.label}</a>`;
                })
                .join('');

            // it visibles sidebar menu //
            sidebar.classList.remove('hidden');
            hamburger.classList.remove('hidden');
            // loops stops here //
            break; 
        }
    }

    // checks if the page is a part of sidebar or not //
    if (!isSubmenuPage) {
        // if the page is not part of sidebar then hides the sidebar //
        sidebar.classList.add('hidden');

        // after it checks main nav links //
        navLinks.forEach(link => {
            // it takes the url from the link //
            const href = link.getAttribute('href');
            // it checks the current page if it match or not //
            if (href === currentPath || (currentPath === '' && href === 'index.html')) {
                // if it get match then it adds active class //
                setActiveMenu(link);
                // if that page doesnot have sidebar then it hides hamburger icon otherwise it shows both sidebar and hamburger icon //
                if (!link.getAttribute('data-sidebar') || !sidebarData[link.getAttribute('data-sidebar')]) {
                    hamburger.classList.add('hidden');
                }
            }
            // and if condittion get false then it does not active the link and it checks next link because we created loop navLinks.forEach //
        });
    }
});