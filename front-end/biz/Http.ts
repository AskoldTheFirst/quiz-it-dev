import { store } from "@/redux/store";
import axios, { AxiosError, AxiosResponse } from "axios";
import { AnswerRequestDto } from "./dto/AnswerRequestDto";

axios.defaults.baseURL = 'https://localhost:7072/api/';
axios.defaults.withCredentials = true;
axios.defaults.timeout = 60000;

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.request.use(config => {
    const token = store.getState().appState.user?.token;
    if (token)
        config.headers.Authorization = `Bearer ${token}`;

    return config;
});

axios.interceptors.response.use(async response => {
    return response;
}, /*not 2xx responce range*/(error: AxiosError) => {
    const { data, status } = error.response as AxiosResponse;

    switch (status) {
        case 400:
            break;
        case 401:
            break;
        case 404:
            break;
        case 500:
            //router.navigate('/server-error', { state: { error: data } });
            break;
        default:
            break;
    }

    console.log("response error: " + status);

    return Promise.reject(data);
});

const requests = {
    get: (url: string, params?: any) => axios.get(url, { params }).then(responseBody),
    post: (url: string, body: object) => axios.post(url, body).then(responseBody),
    put: (url: string, body: object) => axios.put(url, body).then(responseBody),
    delete: (url: string) => axios.delete(url).then(responseBody),
}

const App = {
    initState: () => requests.get('app/init-state'),
    login: (values: any) => requests.post('app/login', values),
    register: (values: any) => requests.post('app/register', values),
    currentUser: () => requests.get('app/currentUser'),
}

const Test = {
    current: () => requests.get('tests/current'),
    create: (technologyName: string) => requests.post(`tests/create?technologyName=${technologyName}`, {}),
    cancel: (testId: number) => requests.post(`tests/cancel?testId=${testId}`, {}),
    answer: (requestModel: AnswerRequestDto) => requests.post('tests/answer', requestModel ),
}

const Http = {
    App,
    Test,
}

export default Http;