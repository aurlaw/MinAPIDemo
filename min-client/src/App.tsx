import { useState } from "react";
import { QueryClient, QueryClientProvider } from "react-query";
import { ReactQueryDevtools } from "react-query/devtools";
import logo from './logo.svg'
import ProductCounter from './components/ProductCounter'
import Products from './components/Products'
import ProductItem from "./components/ProductItem"
import ProductAdd from "./components/ProductAdd"

import './App.css'

import {OpenAPI} from '../references/codegen/index';

const queryClient = new QueryClient();

OpenAPI.BASE = "https://localhost:7219"; // Set this to match your local API endpoint.

export default function App() {
  const [productId, setProductId] = useState('');

  const onProductChange = (id: string) => {
    setProductId(id)
  }; 

  return (
    <QueryClientProvider client={queryClient}>
      <div className="App">
          <header className="App-header">
            <h1>Minimal API Demo</h1>
              <img src={logo} className="App-logo" alt="logo" />
              <p>
                  Total Products: <ProductCounter />
              </p>
              <main>
                <article>
                  <Products setProductId={onProductChange} />
                   <aside>
                     {productId === '' ? 
                      (<div>N/A</div>) :
                      (<ProductItem productId={productId}/>)}
                      <hr/>
                      <ProductAdd />
                   </aside>
                </article>
              </main>
          </header>
      </div>
      <ReactQueryDevtools />
    </QueryClientProvider>
  )
}