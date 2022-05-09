export class Api {
    
    static getProduct = async (id) => {
        const response = await fetch('/api/Products/' + id);
        return await response.json();
    }

    
    static getCategories = async () => {
        const response = await fetch('/api/Categories');
        return await response.json();
    }
}
export default Api;