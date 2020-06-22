import Search from '../pageobjects/Search';
import Favourites from '../pageobjects/Favourites';

describe('The favourites page', () => {
   
    it('should allow removing a pokemon to favourites', () => {
        cy.visit('/')
        let pokemon = 'pikachu';
        let search = new Search(cy);
        search.enterSearch(pokemon);
        search.submit();

        search.addToFavourites();

        search.Nav.goToFavourites();
        let favourites = new Favourites(cy);
        favourites.removeFavourite(pokemon);
        favourites.getFavourite(pokemon).should('not.be.visible');
    });


    it('should preserve favourites on reload', () => {
        cy.visit('/')
        let pokemon = 'pikachu';
        let search = new Search(cy);
        search.enterSearch(pokemon);
        search.submit();

        search.addToFavourites();

        search.Nav.goToFavourites();
        let favourites = new Favourites(cy);
        favourites.getFavourite(pokemon).should('be.visible');

        cy.visit('/favourites');
        favourites.getFavourite(pokemon).should('be.visible');
    });
});