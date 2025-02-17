const itemPath = `item/`;
const recipePath = `recipe/`;
const pricePath = `price/`;
const craftPath = `craft/`;
const worldPath = `world/`;
const getBaseUrl = `http://localhost:55448/`;
const fetchData = async (tabName: string, id: number | null, world: number | null) => {
    try {
        let suffix: string;
        switch (tabName) {
            case 'Items':
                suffix = `${itemPath}${id}`;
                break;
            case 'Recipes':
                suffix = `${recipePath}${id}`;
                break;
            case 'Prices':
                suffix = `${pricePath}${world}/${id}/false`;
                break;
            case 'Profits':
                suffix = `${craftPath}${world}`;
                break;
            case 'World':
                suffix = `${worldPath}`;
                break;
            default:
                suffix = ``;
                break;
        }
        let url = `${getBaseUrl}${suffix}`;
        console.log(`Fetching from url ${url}`)
        let request = new Request(url);
        request.headers.set("Access-Control-Allow-Origin", "*");
        const response = await fetch(request);
        return await response.json();
    } catch (error) {
        console.error('Error fetching data:', error);
    }
};

export type DataFetcherType = {
    fetchData: (tabName: string, id: number | null, world: number | null) => Promise<any>;
};

const DataFetcher: DataFetcherType = {
    fetchData,
};

export default DataFetcher;