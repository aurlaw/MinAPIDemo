import React from 'react'
// import {useQueryClient} from 'react-query'
import {useAddProduct} from '../hooks/useProducts'


export default function ProductAdd() {
    // const queryClient = useQueryClient();
    const [name, setName] = React.useState('');
    const [qty, setQty] = React.useState(0);
    const [price, setPrice] = React.useState(0.00);
    const [error, setError] = React.useState<string>();


    const addProductMutation = useAddProduct(name, qty, price, (success, error) => {
        console.log('add', success);
        console.log('add-err', error);
        if(success) {
            setName('');
            setQty(0);
            setPrice(0.00);
            setError('');
        } else {
            setError(error?.message);
        }
    });

    const addProduct =() => {
        // console.log('name', name);
        // console.log('qty', qty);
        // console.log('price', price);
        addProductMutation.mutate({name,qty,price});
    };
    return(
        <form onSubmit={e => {
            e.preventDefault();
            addProduct();
        }}>
            <label>Name
                <input type="text" placeholder="Name" value={name} onChange={event => setName(event.target.value)} required/>
            </label>
            <label>Qty
                <input type="number" step="1" placeholder="Qty" value={qty} onChange={event => setQty(parseInt(event.target.value))} required/>
            </label>            
            <label>Price
                <input type="number" step="0.01" min="0" placeholder="Price" value={price} onChange={event => setPrice(parseFloat(event.target.value))} required/>
            </label>
            <button className="btn" type="submit">Add</button>

            {error != null ? error : ''}
        </form>
    );
}

