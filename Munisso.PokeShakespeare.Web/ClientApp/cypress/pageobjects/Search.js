import Nav from './Nav';

export default class Search
{
    constructor(cy){
        this.cy = cy;
        this.Nav = new Nav(cy);
    }

    enterSearch(text) {
        cy.get('input[type="text"]').type(text);
    }

    submit() {
        cy.get('input[type="submit"]').click();
    }

    addToFavourites() {
        cy.get('.addFav').click();
    }
};