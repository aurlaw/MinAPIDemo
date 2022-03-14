import { useState, useEffect } from 'react'
import { QueryClient, QueryClientProvider, useQueryClient } from "react-query";
import { ReactQueryDevtools } from "react-query/devtools";

import logo from './logo.svg'
import './App.css'

import {OpenAPI} from '../references/codegen/index';
import {useProducts, useProductsCount} from './hooks/useProducts';

const queryClient = new QueryClient();


OpenAPI.BASE = "https://localhost:7219"; // Set this to match your local API endpoint.

function App() {

  return (
    <QueryClientProvider client={queryClient}>
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <p>
            Total Products: <ProductCounter />
          </p>
          <Products />

        </header>
      </div>
      <ReactQueryDevtools />
    </QueryClientProvider>
  )
}

function ProductCounter() {
  const productCountQuery = useProductsCount();
  return (
    <>
      {productCountQuery.data ?? 0}
    </>
  );
}

function Products() {
  const { status, data, error, isFetching } = useProducts();
  return(
    <div>
      {status === "loading" ? (
        "loading..."
      ) : status === "error" ? (
        <span>Error: {error.message}</span>
      ) : (
        <>
          {data?.map((product) => (
            <div key={product.id}>
              {product.name} - ${product.price}
            </div>
          ))}
            <div>{isFetching ? "Background Updating..." : " "}</div>

        </>
      )}
    </div>
  );

}

export default App
