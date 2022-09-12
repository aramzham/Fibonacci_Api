# Fibonacci_Api
An API capable of generating and returning a subsequence from a sequence of Fibonacci numbers.
<img src="https://www.janisklaise.com/post/fibonacci-soup/fibonacci-soup.jpg" style="height:600px; width:300px" align="right"/>
<div>
Technics used: :hammer_and_wrench:
    <ul>
      <li>Dependency Injection</li>
      <li>Async programming</li>
      <li>Exceptions handling</li>
      <li>Swagger support</li>
      <li>Middlewares</li>
      <li>Unit tests</li>
    </ul>

The API has a single controller with an endpoint accepting the following :five: parameters:
      <ul>
        <li>The index of the first number in Fibonacci sequence that starts subsequence. :first_quarter_moon:</li>
        <li>The index of the last number in Fibonacci sequence that ends subsequence. :last_quarter_moon:</li>
        <li>A boolean, which indicates whether it can use cache or not.</li>
        <li>A time in milliseconds for how long it can run. If generating the first number in subsequence takes longer than that time, the program returns error. :alarm_clock: Otherwise it returns as many numbers as were generated with extra information indicating the timeout occurred.</li>
        <li>A maximum amount of memory the program can use. If, during the execution of the request this amount is reached, the execution aborts. :bomb: The program returns as many generated numbers similarly to the way it does in case of timeout reached.</li>
      </ul>
      
There is a cache for numbers, so that subsequent requests rely on it in order to speed up :muscle: the Fibonacci numbers generation.
The cache is invalidated after a time period :clock7:, where the period is defined in configuration.

<hr width="100%" size="10" color="blue"/>
Things to do: :heavy_check_mark:
<ol>
  <li>add Postman support</li>
  <li>It should schedule the work on two background threads and wait for results asynchronously. The generation of Fibonacci numbers should happen on at least two background threads, where the next number in sequence should be generated on a different thread.</li>
  <li>better unit tests</li>
</ol>
</div>
