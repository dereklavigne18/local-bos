export class Business {
  id: string;
  name: string;
  constructor(id: string, name: string) {
    this.id = id;
    this.name = name;
  }
}

export const fetchBusinesses = async (): Promise<Business[]> => {
  // TODO - Write tests for this
  return await fetch('http://localhost:5280/business').then(
    r => r.json() as Promise<Business[]>,
  );
};
