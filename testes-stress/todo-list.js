import http from 'k6/http';
import { sleep,check  } from 'k6';
import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";
import config from './config.js';

export const options = config.optionsGetTodoList;

export function handleSummary(data) {
	return {
	"todo-list.html": htmlReport(data),
	};
  }

export default function () {

    let urlTodoList = 'api/v1/tasks';

    const responseTasks = http.get(`${config.BASE_URL}${urlTodoList}?active=true`,  config.params);
        
    check(responseTasks, {
        'response code was 200 or 404': (responseTasks) => responseTasks.status == 200 || responseTasks.status == 404,
    });

    let responseTasksSerialize = JSON.parse(responseTasks.body);
    
    if (responseTasksSerialize.status != 200) {
        console.log(responseTasksSerialize);
    }

sleep(1);
}