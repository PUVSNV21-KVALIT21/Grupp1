export class Api {
    
    static getProduct = async (id) => {
        const response = await fetch('/api/Products/' + id);
        return await response.json();
    }
}
export default Api;