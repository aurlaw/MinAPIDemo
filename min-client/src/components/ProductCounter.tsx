import {useProductsCount} from '../hooks/useProducts';


export default function ProductCounter() {
    const productCountQuery = useProductsCount();
    return (
      <>
        {productCountQuery.data ?? 0}
      </>
    );
  }
  