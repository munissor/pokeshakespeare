export default class Nav
{
    constructor(cy){
        this.cy = cy;
    }

    goToSearch() {
        this.cy.get('a[data-test-id="nav-search"]').click();
    }

    goToFavourites() {
        this.cy.get('a[data-test-id="nav-fav"]').click();
    }
};