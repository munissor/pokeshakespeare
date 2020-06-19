import { connect } from 'react-redux'
import { addFavourite } from '../actions/favourite'
import { Search } from '../components/Search'


const mapDispatchToProps = dispatch => ({
    addFavourite: pokemon => dispatch(addFavourite(pokemon))
})

export default connect(null, mapDispatchToProps)(Search);