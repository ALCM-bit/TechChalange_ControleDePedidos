import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  // vus: 5000,
  // duration: '30s',
  stages: [
    { duration: '30s', target: 50 },
    { duration: '1m', target: 100 },
    { duration: '30s', target: 0 },
  ],
  thresholds: {
    http_req_duration: ['p(95)<500'], // 95% of requests must complete below 500ms
    http_req_failed: ['rate<0.01'], // http errors should be less than 1%
  },
};

export default function () {
  const url = 'http://127.0.0.1:5187/health'
  const res = http.get(url);

  check(res, {
    'status is 200': (r) => r.status === 200,
  })
  sleep(1);
}