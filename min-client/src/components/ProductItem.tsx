import {useProduct} from '../hooks/useProducts';

type AppProps = {
    productId: string;
  }; 
  
  export default function ProductItem({productId}: AppProps) {
    const { isFetching, ...queryInfo } = useProduct(productId);
    return(
      <div>
      {queryInfo.isLoading ? (
        "loading..."
      ) : queryInfo.isError ? (
        <span>Error: {queryInfo.error?.message}</span>
      ) : (
        <>
          {queryInfo.data?.name}<br/>
          {queryInfo.data?.quantity}<br/>
          ${queryInfo.data?.price}
          <div>{isFetching ? "Background Updating..." : " "}</div>
        </>
      )}
      </div>
    );
  }
   