using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TqlPoWebApi.Data;
using TqlPoWebApi.Models;

namespace TqlPoWebApi.Controllers
{
    //EVERYTHING BUT THE LOG IN IS GENERATED  THANK GOODNESS
    //first line is for the front end web to tw controllers , 2nd line is another attribute evaluates to employees plural
    //takes the class name of the controller and has to identify the controller, class name and then the controller, api controller is a provided 
   //attribute do not change ...all the way down to Public employee controler context 4 lines down
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly PoContext _context;
        //const  cuz of read only immed below yu can only pass data thru below constructor
        public EmployeesController(PoContext context)
        {
            _context = context;
        }

        //get: API/Employees/gpdoud/password
        [HttpGet("{login}/{password}")]
        public async Task<ActionResult<Employee>> Login(string login, string password)
        {
            var empl = await _context.Employee
                            .SingleOrDefaultAsync(e => e.Login == login && e.Password == password);
            if (empl == null)
            {
                return NotFound();
            }
            return Ok(empl);
        }
        // GET: api/Employees  
        [HttpGet]
        /*method immed below is async call, everthing that is async at the minimumyou will return a task
        //action result is a class that allows us to return different kinds of data, the IEnumerable gives us the flexibility to change 
        // it to an array; I enum allows you to iterate thru it in a foreach loop.  the dot net library allows you to return either of 
        and the return type is valid..the task is the action result that allows you to return employee data, not one employee but a collection
        of employees, and then use the foreach loop. Can return a colection or if their are none it can return a a valid message cuz of action result
        class.  Action result is a base class and the derived data can be a message as well as the data if it exists...because of asynch 
        everything must be inside of Task...if asynch you will always return a Task...method name is GetEmployee but is is not being called here
        Get Employee returns await cuz it is asynch...call to database is thru _context and shoud therefore incl asynch
        */
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            return await _context.Employee.ToListAsync();
        }

        // GET: api/Employees/5
        /*  id is PK id of employee, here it is a route variable, so is is wrapped in curly braces, allows us to pass thru data to controller
         *  and so the id is declared in the http method  each route variable needs a parameter and the name has to match exactly id tothe method
         *  (int id)....the Task below is only returning one employee...this is returning json data
         *  
          */
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5  THE UPDATE
        /*
         * We have to pass in the id of the record we are changing along with so we have safety so we do not pass in the 
         * wrong record (int id, Employee employee)
        */
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.ID)
            {
                return BadRequest();
            }
            //read the data first and then change what you want to change...inside the cache of EntityState entity framework and sets it to be
            // modified.  this statement is checks data to make certain it should be or can be updated so State becomes Modified.
            // takes data passed it and treats it as if youread it in the data base and then passes it so youcan do the update
            _context.Entry(employee).State = EntityState.Modified;
            //this try catch block is catching exception class DDUpdateConcurrencyException. so you don't update something eg that someone else
            //has updated the row like if someone else deleted it first, with this your program does not blow up by deleting something that 
            // no longer exists
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            //return is no content cuz you are updating an existing record
            return NoContent();
        }

        // POST: api/Employees  THIS IS THE ADD METHOD
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        /*  the post method is expecting the body to have the data the base controller ends with Employee in (Employee employee)
         * asynch is used cuz the path to the data base might take longer time and so we do not freeze up our program...
         * CreatedAtAction is a built in function for us to use when needed like here 
         * */
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.ID }, employee);
        }

        // DELETE: api/Employees/5  THE REMOVE
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.ID == id);
        }
    }
}
