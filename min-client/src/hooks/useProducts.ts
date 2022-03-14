import { useQuery, UseQueryOptions } from "react-query";
import { AxiosError } from 'axios';
import {Product, ProductsApiService} from "../../references/codegen/index";

async function getProducts(): Promise<Product[]> {
    return await ProductsApiService.getProducts();
}
  
export function useProducts() {
    return useQuery('products', getProducts);
}
export function useProductsCount() {
    return useQuery('products', getProducts, {
        select: (products) => products.length,
        notifyOnChangeProps: ['data']
    });
}
