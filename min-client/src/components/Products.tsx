// import  from "react-query";
import {useProducts} from '../hooks/useProducts';

type AppProps = {
  setProductId: (id: string) => void;
}; 

export default function Products({setProductId}: AppProps) {
    const { isFetching, ...queryInfo } = useProducts();

    return(
      <div className="products">
      {queryInfo.isLoading ? (
        "loading..."
      ) : queryInfo.isError ? (
        <span>Error: {queryInfo.error?.message}</span>
      ) : (
        <>
          {queryInfo.data?.map((product) => (
              <a key={product.id} className="product-item" onClick={() => setProductId(product.id!)}>
                <span>{product.name}</span>
                <span>{product.quantity}</span>
                <span>${product.price}</span>
              </a>
          ))}
            {isFetching ? "Background Updating..." : " "}
        </>
      )}
      </div>
    );
  }
  