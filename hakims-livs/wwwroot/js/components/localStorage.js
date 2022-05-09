export class LocalStorage {

    static Get = (key) => {
        let value;
        if (JSON.parse(localStorage.getItem(key))) {
            value = JSON.parse(localStorage.getItem(key));
        }
        return value;
    }
    
    static Set = (key, value) => {
        window.localStorage.setItem(key,  JSON.stringify(value))
    }
}

export default LocalStorage;
