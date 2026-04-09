import { store } from "@/redux/store";
import axios, { AxiosError, AxiosResponse } from "axios";
import { AnswerRequestDto } from "./dto/AnswerRequestDto";
import { StatisticsRequestDto } from "./dto/StatisticsRequestDto";

//axios.defaults.baseURL = 'https://localhost:7072/api/';
axios.defaults.baseURL = 'https://quiz-it-api-a6fsfcdfg3gnb6ee.westeurope-01.azurewebsites.net/api/';
axios.defaults.withCredentials = false;
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
    create: (topicName: string) => requests.post(`tests/create?topicName=${topicName}`, {}),
    cancel: (testId: number) => requests.post(`tests/cancel?testId=${testId}`, {}),
    answer: (requestModel: AnswerRequestDto) => requests.post('tests/answer', requestModel),
    complete: (testId: number) => requests.post(`tests/complete?testId=${testId}`, {}),
}

const Statistics = {
    page: (requestMode: StatisticsRequestDto) => requests.get('statistics/page', requestMode),
    profile: () => requests.get('statistics/profile'),
}

const Http = {
    App,
    Test,
    Statistics,
}

export default Http;