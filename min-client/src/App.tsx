import { useState, useEffect } from 'react'
import logo from './logo.svg'
import './App.css'

import {OpenAPI, Product, MinApiDemoVersion1000CultureNeutralPublicKeyTokenNullService as ApiService} from "../references/codegen/index";

OpenAPI.BASE = "https://localhost:7219"; // Set this to match your local API endpoint.

async function getProducts(): Promise<Product[]> {
  return await ApiService.getProducts();
}

function App() {
  const [count, setCount] = useState(0)
  const [total, setTotal] = useState(0)

  // Similar to componentDidMount and componentDidUpdate:
  useEffect(() => {
    (async () => {
      const products = await getProducts();
      setTotal(products.length);
    })()
  }, []);  
/*
  useEffect(() => {
    (async () => {
      const products = await api.index()
      setFilteredProducts(products)
      setProducts(products)
    })()

    return () => {
      unsubscribeOrRemoveEventHandler() /
    }
  }, [])

*/

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>Hello Vite + React + Typescript!</p>
        <p>
          <button type="button" onClick={() => setCount((count) => count + 1)}>
            count is: {count}
          </button>
        </p>
        <p>
          Total Products: {total}
        </p>
        <p>
          <a
            className="App-link"
            href="https://reactjs.org"
            target="_blank"
            rel="noopener noreferrer"
          >
            Learn React
          </a>
          {' | '}
          <a
            className="App-link"
            href="https://vitejs.dev/guide/features.html"
            target="_blank"
            rel="noopener noreferrer"
          >
            Vite Docs
          </a>
        </p>
      </header>
    </div>
  )
}

export default App
