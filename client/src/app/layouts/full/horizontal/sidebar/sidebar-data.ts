import { NavItem } from '../../vertical/sidebar/nav-item/nav-item';

export const navItems: NavItem[] = [
  {
    navCap: 'Home',
  },
  {
    displayName: 'Dashboards',
    iconName: 'home',
    route: 'dashboards',
    children: [
      {
        displayName: 'Analytical',
        iconName: 'point',
        route: 'demo/dashboards/dashboard1',
      },
      {
        displayName: 'eCommerce',
        iconName: 'point',
        route: 'demo/dashboards/dashboard2',
      },
    ],
  },
  {
    displayName: 'Apps',
    iconName: 'apps',
    route: 'apps',
    ddType: '',
    children: [
      {
        displayName: 'Chat',
        iconName: 'point',
        route: 'apps/chat',
      },
      {
        displayName: 'Calendar',
        iconName: 'point',
        route: 'apps/calendar',
      },
      {
        displayName: 'Email',
        iconName: 'point',
        route: 'apps/email/inbox',
      },
      {
        displayName: 'Contacts',
        iconName: 'point',
        route: 'apps/contacts',
      },
      {
        displayName: 'Courses',
        iconName: 'point',
        route: 'apps/courses',
      },
      {
        displayName: 'Employee',
        iconName: 'point',
        route: 'apps/employee',
      },
      {
        displayName: 'Notes',
        iconName: 'point',
        route: 'apps/notes',
      },
      {
        displayName: 'Tickets',
        iconName: 'point',
        route: 'apps/tickets',
      },
      {
        displayName: 'Invoice',
        iconName: 'point',
        route: 'apps/invoice',
      },
      {
        displayName: 'ToDo',
        iconName: 'point',
        route: 'apps/todo',
      },
      {
        displayName: 'Taskboard',
        iconName: 'point',
        route: 'apps/taskboard',
      },
      {
        displayName: 'Blog',
        iconName: 'point',
        route: 'apps/blog',
        children: [
          {
            displayName: 'Post',
            iconName: 'point',
            route: 'apps/blog/post',
          },
          {
            displayName: 'Detail',
            iconName: 'point',
            route: 'apps/blog/detail/Early Black Friday Amazon deals: cheap TVs, headphones, laptops',
          },
        ],
      },
    ],
  },
  {
    displayName: 'Ui',
    iconName: 'components',
    route: 'ui-components',
    ddType: '',
    children: [
      {
        displayName: 'Badge',
        iconName: 'point',
        route: 'demo/demo/ui-components/badge',
      },
      {
        displayName: 'Expansion Panel',
        iconName: 'point',
        route: 'demo/ui-components/expansion',
      },
      {
        displayName: 'Chips',
        iconName: 'point',
        route: 'demo/ui-components/chips',
      },
      {
        displayName: 'Dialog',
        iconName: 'point',
        route: 'demo/ui-components/dialog',
      },
      {
        displayName: 'Lists',
        iconName: 'point',
        route: 'demo/ui-components/lists',
      },
      {
        displayName: 'Divider',
        iconName: 'point',
        route: 'demo/ui-components/divider',
      },
      {
        displayName: 'Menu',
        iconName: 'point',
        route: 'demo/ui-components/menu',
      },
      {
        displayName: 'Paginator',
        iconName: 'point',
        route: 'demo/ui-components/paginator',
      },
      {
        displayName: 'Progress Bar',
        iconName: 'point',
        route: 'demo/ui-components/progress',
      },
      {
        displayName: 'Progress Spinner',
        iconName: 'point',
        route: 'demo/ui-components/progress-spinner',
      },
      {
        displayName: 'Ripples',
        iconName: 'point',
        route: 'demo/ui-components/ripples',
      },
      {
        displayName: 'Slide Toggle',
        iconName: 'point',
        route: 'demo/ui-components/slide-toggle',
      },
      {
        displayName: 'Slider',
        iconName: 'point',
        route: 'demo/ui-components/slider',
      },
      {
        displayName: 'Snackbar',
        iconName: 'point',
        route: 'demo/ui-components/snackbar',
      },
      {
        displayName: 'Tabs',
        iconName: 'point',
        route: 'demo/ui-components/tabs',
      },
      {
        displayName: 'Toolbar',
        iconName: 'point',
        route: 'demo/ui-components/toolbar',
      },
      {
        displayName: 'Tooltips',
        iconName: 'point',
        route: 'demo/ui-components/tooltips',
      },
    ],
  },
  {
    displayName: 'Pages',
    iconName: 'clipboard',
    route: '',
    children: [
      {
        displayName: 'Treeview',
        iconName: 'point',
        route: 'demo/theme-pages/treeview',
      },
      {
        displayName: 'Pricing',
        iconName: 'point',
        route: 'demo/theme-pages/pricing',
      },
      {
        displayName: 'Account Setting',
        iconName: 'point',
        route: 'demo/theme-pages/account-setting',
      },
      {
        displayName: 'FAQ',
        iconName: 'point',
        route: 'demo/theme-pages/faq',
      },
      {
        displayName: 'Landingpage',
        iconName: 'point',
        route: 'demo/landingpage',
      },
      {
        displayName: 'Widgets',
        iconName: 'point',
        route: 'widgets',
        children: [
          {
            displayName: 'Cards',
            iconName: 'point',
            route: 'demo/widgets/cards',
          },
          {
            displayName: 'Banners',
            iconName: 'point',
            route: 'demo/widgets/banners',
          },
          {
            displayName: 'Charts',
            iconName: 'point',
            route: 'demo/widgets/charts',
          },
        ],
      },
      {
        displayName: 'Charts',
        iconName: 'point',
        route: 'charts',
        children: [
          {
            displayName: 'Line',
            iconName: 'point',
            route: '/demo/charts/line',
          },
          {
            displayName: 'Gredient',
            iconName: 'point',
            route: '/demo/charts/gredient',
          },
          {
            displayName: 'Area',
            iconName: 'point',
            route: '/demo/charts/area',
          },
          {
            displayName: 'Candlestick',
            iconName: 'point',
            route: '/demo/charts/candlestick',
          },
          {
            displayName: 'Column',
            iconName: 'point',
            route: '/demo/charts/column',
          },
          {
            displayName: 'Doughnut & Pie',
            iconName: 'point',
            route: '/demo/charts/doughnut-pie',
          },
          {
            displayName: 'Radialbar & Radar',
            iconName: 'point',
            route: '/demo/charts/radial-radar',
          },
        ],
      },
      {
        displayName: 'Auth',
        iconName: 'point',
        route: '/',
        children: [
          {
            displayName: 'Login',
            iconName: 'point',
            route: '/authentication',
            children: [
              {
                displayName: 'Login 1',
                iconName: 'point',
                route: '/authentication/login',
              },
              {
                displayName: 'Boxed Login',
                iconName: 'point',
                route: '/authentication/boxed-login',
              },
            ],
          },
          {
            displayName: 'Register',
            iconName: 'point',
            route: '/authentication',
            children: [
              {
                displayName: 'Login 1',
                iconName: 'point',
                route: '/authentication/side-register',
              },
              {
                displayName: 'Boxed Login',
                iconName: 'point',
                route: '/authentication/boxed-register',
              },
            ],
          },
          {
            displayName: 'Forgot Password',
            iconName: 'point',
            route: '/authentication',
            children: [
              {
                displayName: 'Side Forgot Password',
                iconName: 'point',
                route: '/authentication/side-forgot-pwd',
              },
              {
                displayName: 'Boxed Forgot Password',
                iconName: 'point',
                route: '/authentication/boxed-forgot-pwd',
              },
            ],
          },
          {
            displayName: 'Two Steps',
            iconName: 'point',
            route: '/authentication',
            children: [
              {
                displayName: 'Side Two Steps',
                iconName: 'point',
                route: '/authentication/side-two-steps',
              },
              {
                displayName: 'Boxed Two Steps',
                iconName: 'point',
                route: '/authentication/boxed-two-steps',
              },
            ],
          },
          {
            displayName: 'Error',
            iconName: 'point',
            route: '/authentication/error',
          },
          {
            displayName: 'Maintenance',
            iconName: 'point',
            route: '/authentication/maintenance',
          },
        ],
      },
    ],
  },
  {
    displayName: 'Forms',
    iconName: 'file-description',
    route: 'forms',
    children: [
      {
        displayName: 'Form elements',
        iconName: 'point',
        route: 'demo/forms/forms-elements',
        children: [
          {
            displayName: 'Autocomplete',
            iconName: 'point',
            route: 'demo/forms/forms-elements/autocomplete',
          },
          {
            displayName: 'Button',
            iconName: 'point',
            route: 'demo/forms/forms-elements/button',
          },
          {
            displayName: 'Checkbox',
            iconName: 'point',
            route: 'demo/forms/forms-elements/checkbox',
          },
          {
            displayName: 'Radio',
            iconName: 'point',
            route: 'demo/forms/forms-elements/radio',
          },
          {
            displayName: 'Datepicker',
            iconName: 'point',
            route: 'demo/forms/forms-elements/datepicker',
          },
        ],
      },
      {
        displayName: 'Form Layouts',
        iconName: 'point',
        route: '/demo/forms/form-layouts',
      },
      {
        displayName: 'Form Horizontal',
        iconName: 'point',
        route: '/demo/forms/form-horizontal',
      },
      {
        displayName: 'Form Vertical',
        iconName: 'point',
        route: '/demo/forms/form-vertical',
      },
      {
        displayName: 'Form Wizard',
        iconName: 'point',
        route: '/demo/forms/form-wizard',
      },
    ],
  },
  {
    displayName: 'Tables',
    iconName: 'layout',
    route: 'tables',
    children: [
      {
        displayName: 'Basic Table',
        iconName: 'point',
        route: 'demo/tables/basic-table',
      },
      {
        displayName: 'Dynamic Table',
        iconName: 'point',
        route: 'demo/tables/dynamic-table',
      },
      {
        displayName: 'Expand Table',
        iconName: 'point',
        route: 'demo/tables/expand-table',
      },
      {
        displayName: 'Filterable Table',
        iconName: 'point',
        route: 'demo/tables/filterable-table',
      },
      {
        displayName: 'Footer Row Table',
        iconName: 'point',
        route: 'demo/tables/footer-row-table',
      },
      {
        displayName: 'HTTP Table',
        iconName: 'point',
        route: 'demo/tables/http-table',
      },
      {
        displayName: 'Mix Table',
        iconName: 'point',
        route: 'demo/tables/mix-table',
      },
      {
        displayName: 'Multi Header Footer',
        iconName: 'point',
        route: 'demo/tables/multi-header-footer-table',
      },
      {
        displayName: 'Pagination Table',
        iconName: 'point',
        route: 'demo/tables/pagination-table',
      },
      {
        displayName: 'Row Context Table',
        iconName: 'point',
        route: 'demo/tables/row-context-table',
      },
      {
        displayName: 'Selection Table',
        iconName: 'point',
        route: 'demo/tables/selection-table',
      },
      {
        displayName: 'Sortable Table',
        iconName: 'point',
        route: 'demo/tables/sortable-table',
      },
      {
        displayName: 'Sticky Column',
        iconName: 'point',
        route: 'demo/tables/sticky-column-table',
      },
      {
        displayName: 'Sticky Header Footer',
        iconName: 'point',
        route: 'demo/tables/sticky-header-footer-table',
      },
      {
        displayName: 'Data table',
        iconName: 'border-outer',
        route: '/demo/datatable/kichen-sink',
      },
    ],
  },
];
