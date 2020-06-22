
import reducer from './favourites';

describe('Favourites reducer', () => {
    
    it('should return the initial state', () => {
      let fav = reducer(undefined, {});
      expect(fav).toEqual([]);
    })
  
    it('should handle ADD_FAVOURITE', () => {
      let pokemon = {
        pokemon: 'pikachu',
        description: 'test'
      };

      let fav = reducer([], {
        type: 'ADD_FAVOURITE',
        pokemon,
      });

      expect(fav).toEqual([pokemon]);
    });


    it('should handle REMOVE_FAVOURITE', () => {
        let pokemon = {
          pokemon: 'pikachu',
          description: 'test'
        };
  
        let fav = reducer([pokemon], {
          type: 'REMOVE_FAVOURITE',
          pokemonName: pokemon.pokemon
        });
  
        expect(fav).toEqual([]);
    });


    it('should not add the same pokemon twice', () => {
        let pokemon = {
          pokemon: 'pikachu',
          description: 'test'
        };
  
        let fav = reducer([pokemon], {
            type: 'ADD_FAVOURITE',
            pokemon,
          });
  
        expect(fav).toEqual([pokemon]);
    });

    it('should not fail removing a pokemon not in the favourites', () => {
        let pokemonName = 'pikachu';
  
        let fav = reducer([], {
            type: 'REMOVE_FAVOURITE',
            pokemonName,
        });
  
        expect(fav).toEqual([]);
    });
  })