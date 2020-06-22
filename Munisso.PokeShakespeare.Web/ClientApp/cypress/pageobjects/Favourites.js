import Nav from './Nav';

export default class Favourites
{
    constructor(cy){
        this.cy = cy;
        this.Nav = new Nav(cy);
    }

    getFavourite(pokemon) {
        return cy.get(`[data-test-id="fav-${pokemon}"]`);
    }

    removeFavourite(pokemon) {
        cy.get(`[data-test-id="fav-${pokemon}"] button`).click();
    }
};