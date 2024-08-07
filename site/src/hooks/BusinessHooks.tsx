import { useEffect, useState } from 'react';
import { Business, fetchBusinesses } from 'src/api/BusinessClient';

export class FetchBusinessesState {
  status: string;
  businesses: Business[];
  constructor(status: string, businesses: Business[]) {
    this.status = status;
    this.businesses = businesses;
  }
}

export const useBusinesses = (): FetchBusinessesState => {
  const [status, setStatus] = useState('idle');
  const [businesses, setBusinesses] = useState<Business[]>([]);

  useEffect(() => {
    const doFetchBusinesses = async () => {
      setStatus('fetching');

      const businesses = await fetchBusinesses();

      setBusinesses(businesses);
      setStatus('fetched');
    };

    doFetchBusinesses();
  }, []);

  return { status, businesses };
};
