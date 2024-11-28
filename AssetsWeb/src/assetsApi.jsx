const baseUrl = 'https://localhost:7195';

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
    
    addAsset: async (asset) => {
        var reqUrl = baseUrl + '/api/Assets';
        const response = await fetch(reqUrl,
            {
                method: 'POST',
                credentials: 'include',
                mode: 'cors',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(asset)
            });
        return response.json();
    },

    deleteAssetById: async (id) => {
        var reqUrl = baseUrl + '/api/Assets/' + id;
        const response = await fetch(reqUrl, { method: 'DELETE', credentials: 'include', mode: 'cors' });
        return response.json();
    },

};