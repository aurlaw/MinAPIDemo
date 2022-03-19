import { useQuery, useMutation, useQueryClient, UseQueryResult, UseMutationResult } from "react-query";
import { AxiosError } from 'axios';
import {Product, ProductsApiService} from "../../references/codegen/index";
import { v4 as uuidv4 } from 'uuid';

async function getProducts(): Promise<Product[]> {
    return await ProductsApiService.getProducts();
}
async function getProductById(id:string): Promise<Product> {
    return await ProductsApiService.getProductById({id});
}
async function createProduct(product:Product) {
    return await ProductsApiService.createProduct({requestBody:product});
}
  
export function useProducts(): UseQueryResult<Product[],AxiosError> {
    return useQuery('products', getProducts);
}
export function useProductsCount(): UseQueryResult<number,AxiosError> {
    return useQuery('products', getProducts, {
        select: (products) => products.length,
        notifyOnChangeProps: ['data']
    });
}
export function useProduct(id:string): UseQueryResult<Product,AxiosError> {
    return useQuery(["product", id], () => getProductById(id));
}

export function useAddProduct(name:string, quantity:number, price:number
    , onComplete?:(success:boolean, error?: AxiosError) => void):UseMutationResult<any,AxiosError,any> {
    const queryClient = useQueryClient();
    const product:Product = {
        id:uuidv4(),
        name,
        quantity,
        price
    };
    return useMutation(() => createProduct(product),{
        onMutate:async () => {
            //products
            await queryClient.cancelQueries('products')

            const previousProducts = queryClient.getQueryData<Product[]>('products')
            // // Optimistically update to the new value
            // if (previousProducts) {
            //     const updated = [...previousProducts, product]

            //     queryClient.setQueryData<Product[]>('products', {
            //         ...updated
            //     })
            // }      
            return {previousProducts}
        },
        onSuccess:  (data, variables, context) => {
            if(onComplete) {
                onComplete(true)
            }      
        },
        // If the mutation fails, use the context returned from onMutate to roll back
        onError: (err, variables, context) => {
            // if (context?.previousProducts) {
            //     queryClient.setQueryData<Product[]>('products', context.previousProducts)
            // }
            if(onComplete) {
                onComplete(false, err);
            }      
        },
        // Always refetch after error or success:
        onSettled: () => {
            queryClient.invalidateQueries('products')
        },        
    });
}
