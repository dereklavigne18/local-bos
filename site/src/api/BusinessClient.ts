export interface Business {
    id: string;
    name: string;
}


async function fetchBusinesses(): Promise<Business[]> {
    return await fetch('http://localhost:5280/business')
        .then(r => r.json() as Promise<Business[]>);
}

export default fetchBusinesses; 