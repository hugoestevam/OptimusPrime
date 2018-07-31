import { MainPage } from './app.po';

describe('ng-dashboard App', () => {
  let page: MainPage;

  beforeEach(() => {
    page = new MainPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
