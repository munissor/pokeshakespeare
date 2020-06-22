import Search from '../pageobjects/Search';
import Favourites from '../pageobjects/Favourites';

describe('The search page', () => {

    it('should find a pokemon correctly', () => {
        cy.visit('/')
        cy.get('input[type="text"]')
            .type('pikachu');

        cy.get('input[type="submit"]').click();

        cy.get('h2').should((h2) => {
            expect(h2).to.have.length(1)
            expect(h2.first()).to.contain('pikachu')
        });
    });

    it('should show an error if a pokemon does not exist', () => {
        cy.visit('/')
        cy.get('input[type="text"]')
            .type('invalid');

        cy.get('input[type="submit"]').click();

        cy.get('.error').should('be.visible');
    });


    it('should allow saving a pokemon to favourites', () => {
        cy.visit('/')
        let pokemon = 'pikachu';
        let search = new Search(cy);
        search.enterSearch(pokemon);
        search.submit();

        search.addToFavourites();

        search.Nav.goToFavourites();
        let favourites = new Favourites(cy);
        favourites.getFavourite(pokemon).should('be.visible');
    });
});