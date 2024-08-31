import logo from '../assets/githublog.png';

function Sidebar() {
  return (
    <div className="d-flex flex-column flex-shrink-0 p-3 text-white bg-dark h-100">
      <a href="/" className="d-flex align-items-center mb-3 mb-md-0 me-md-auto text-white text-decoration-none">
        <img src={logo} alt='logo' width="40" height="32"></img>
        <span className="fs-4">CV hub</span>
      </a>
      <hr />
      <ul className="nav nav-pills flex-column mb-auto">
        <li className="nav-item" >
          <a href='/' className="nav-link text-white" >
            <i className="bi bi-house me-2" width="16" height="16"></i>
            Home
          </a>
        </li>
        <li>
          <a href='/create' className="nav-link text-white">
            <i className="bi bi-plus-square me-2" width="16" height="16"></i>
            New
          </a>
        </li>
      </ul>
      <hr />

    </div>
  );
}

export default Sidebar;
