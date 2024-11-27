const baseUrl = 'http://localhost:7195';

export const AssetsApi = {

    getCategories: async () => {
        var reqUrl = baseUrl + '/api/Categories';
        const response = await fetch(reqUrl, { method: 'GET', credentials: 'include', mode: 'cors' });
        return response.json();
    },

    getCategoryById: async (id) => {
        var reqUrl = baseUrl + '/api/Categories/' + id;
        const response = await fetch(reqUrl, { method: 'GET', credentials: 'include', mode: 'cors' });
        return response.json();
    },

    getAssets: async () => {
        var reqUrl = baseUrl + '/api/Assets';
        const response = await fetch(reqUrl, { method: 'GET', credentials: 'include', mode: 'cors' });
        return response.json();
    },

    getAssetById: async (id) => {
        var reqUrl = baseUrl + '/api/Assets/' + id;
        const response = await fetch(reqUrl, { method: 'GET', credentials: 'include', mode: 'cors' });
        return response.json();
    },
    
    

};